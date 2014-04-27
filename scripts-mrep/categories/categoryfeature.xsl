<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:query('select cf.* from str_categoryfeature cf inner join str_category c on c.id = cf.categoryid')">
          <categoryfeature>
            <categoryfeatureid m:left="-6" m:top="162">pk:categoryfeature(<xsl:value-of select="id" />)</categoryfeatureid>
            <categoryid m:left="-130" m:top="178">
              <xsl:value-of select="categoryid" />
            </categoryid>
            <featureid m:left="-8" m:top="193">
              <xsl:value-of select="featureid" />
            </featureid>
            <featuregroupid m:left="-133" m:top="208">
              <xsl:value-of select="featuregroupid" />
            </featuregroupid>
            <overview m:left="-10" m:top="226">
              <xsl:value-of select="overview" />
            </overview>
            <manditory m:left="-135" m:top="241">
              <xsl:value-of select="manditory" />
            </manditory>
            <sequenceid m:left="-11" m:top="258">
              <xsl:value-of select="sequenceid" />
            </sequenceid>
            <isvariant m:left="-136" m:top="274">0</isvariant>
          </categoryfeature>
        </xsl:for-each>
      </mrep3>
    </target>
  </xsl:template>
</xsl:stylesheet>