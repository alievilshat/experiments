<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:mrep="http://www.navitas.nl/2014/Mapper/dbaccess/mrep">
  <xsl:template match="/">
    <target>
      <mrep3>
        <xsl:for-each select="mrep:query('select cf.id, c.id as categoryid, cf.featureid, cf.sequenceid from str_categoryfeature cf inner join str_category c on c.id != 180')">
          <categoryfeature>
            <categoryfeatureid m:left="-1" m:top="128">pk:categoryfeature(<xsl:value-of select="id" />-<xsl:value-of select="categoryid" />)</categoryfeatureid>
            <categoryid m:left="-130" m:top="178">
              <xsl:value-of select="categoryid" />
            </categoryid>
            <featureid m:left="-8" m:top="193">
              <xsl:value-of select="featureid" />
            </featureid>
            <featuregroupid m:left="-133" m:top="208">
              <xsl:value-of select="featuregroupid" />
            </featuregroupid>
            <overview m:left="-10" m:top="226">0</overview>
            <manditory m:left="-135" m:top="241">0</manditory>
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