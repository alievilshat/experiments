<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:query('select s.objecttypeid, s.description, s.value, w.businessid from sys_settings s inner join cms_website w on w.websiteid = s.websiteid')">
          <imagesettings>
            <objecttypeid m:left="-44" m:top="172">
              <xsl:value-of select="objecttypeid" />
            </objecttypeid>
            <description m:left="-170" m:top="188">
              <xsl:value-of select="description" />
            </description>
            <value m:left="-38" m:top="206">
              <xsl:value-of select="value" />
            </value>
            <businessid m:left="-172" m:top="228">
              <xsl:value-of select="businessid" />
            </businessid>
            <imagesettingsid m:left="-206" m:top="94">pk:imagesettings(<xsl:value-of select="businessid" />-<xsl:value-of select="objecttypeid" />)</imagesettingsid>
          </imagesettings>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
</xsl:stylesheet>