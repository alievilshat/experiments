<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:m="http://www.navitas.nl/2014/Mapper" xmlns:bodyview="http://www.navitas.nl/2014/Mapper/dbaccess/bodyview">
  <xsl:template match="/">
    <target>
      <bodyview3>
        <xsl:for-each select="bodyview:query('select cf.featureid, c.id as categoryid  from str_categoryfeature cf, str_category c where cf.categoryid = 1')">
          <categoryfeature>
            <categoryfeatureid m:left="-72" m:top="58">pk:categoryfeature(<xsl:value-of select="categoryid" />-<xsl:value-of select="featureid" />)</categoryfeatureid>
            <categoryid m:left="-153" m:top="136">
              <xsl:value-of select="categoryid" />
            </categoryid>
            <featureid m:left="6" m:top="160">
              <xsl:value-of select="featureid" />
            </featureid>
            <overview m:left="-10" m:top="226">0</overview>
            <manditory m:left="-135" m:top="241">0</manditory>
            <sequenceid m:left="-11" m:top="258">0</sequenceid>
            <isvariant m:left="-136" m:top="274">0</isvariant>
          </categoryfeature>
        </xsl:for-each>
      </bodyview3>
    </target>
  </xsl:template>
</xsl:stylesheet>